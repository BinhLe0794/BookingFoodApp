//
//  HttpClient.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import Foundation
import UIKit


struct HttpClient {

    static let shared = HttpClient()

    private init() { }


    func makingHttpRequest<T: Decodable> (route: Route,
                                          method: Method,
                                          parameters: [String: Any]? = nil,
                                          fileImage: UIImage? = nil,
                                          completion: @escaping (Result<T, Error>) -> Void) {
        //CREATE REQUEST
        guard let request = createRequest(route: route, method: method, parameters: parameters, fileData: fileImage) else {
            completion(.failure(AppError.unknownError))
            return
        }
        //HANDLE RESPONSE
        URLSession.shared.dataTask(with: request) { data, response, error in
            var result: Result<Data, Error>?

//            let httpResponse = response as? HTTPURLResponse

            if let data = data {
                result = .success(data)
                let responseString = String(data: data, encoding: .utf8) ?? "Could not stringify data"
                print("responseRAW: \(responseString)")
            } else if let error = error {
                result = .failure(error)
                print("ErrorRAW : \(error.localizedDescription)")
            }

            //RETURN THE RESULT
            DispatchQueue.main.async {
                self.handleResponse(result: result, completion: completion)
            }

        }.resume()
    }

    func handleResponse<T: Decodable>(result: Result<Data, Error>?,
                                      completion: (Result<T, Error>) -> Void) {
        guard let result = result else {
            completion(.failure(AppError.unknownError))
            return
        }
        switch result {
        case .success(let data):
            let decoder = JSONDecoder()
            let response = try? decoder.decode(ApiResult<T>.self, from: data)

            guard response != nil else {
                let response = try? decoder.decode(ApiResult<Bool>.self, from: data)
                print("decoded_ApiResult_bool >>>>: \(response!)")
                if response != nil {
                    completion(.failure(AppError.serverError((response?.message)!)))
                } else {
                    completion(.failure(AppError.errorDecoding))
                }
                return
            }
            if response!.isSuccessed {
                if let decodedData = response?.resultObj {
                    print("decoded_resultObj >>>>: \(decodedData)")
                    completion(.success(decodedData))
                } else {
                    completion(.failure(AppError.serverError(response?.message ?? "Error")))
                }
            } else {
                completion(.failure(AppError.serverError(response?.message ?? "Error")))
            }

        case .failure(let error):
            completion(.failure(error))
        }
    }

    func createDataBody(withParameters params: [String: Any]?, media: [Media]?, boundary: String) -> Data {

        let lineBreak = "\r\n"
        var body = Data()

        if let params = params {
            for (key, value) in params {
                body.append("--\(boundary + lineBreak)" .data(using: .utf8)!)
                body.append("Content-Disposition: form-data; name=\"\(key)\"\(lineBreak + lineBreak)".data(using: .utf8)!)
                body.append("\(value as! String + lineBreak)" .data(using: .utf8)!)
            }
        }

        if let media = media {
            for photo in media {
                body.append("--\(boundary + lineBreak)".data(using: .utf8)!)
                body.append("Content-Disposition: form-data; name=\"\(photo.key)\"; filename=\"\(photo.filename)\"\(lineBreak)".data(using: .utf8)!)
                body.append("Content-Type: \(photo.mimeType + lineBreak + lineBreak)".data(using: .utf8)!)
                body.append(photo.data)
                body.append(lineBreak.data(using: .utf8)!)
            }
        }

        body.append("--\(boundary)--\(lineBreak)" .data(using: .utf8)!)

        return body
    }

    public func createRequest(route: Route,
                              method: Method,
                              parameters: [String: Any]? = nil,
                              fileData: UIImage? = nil) -> URLRequest? {

        let urlString = Route.baseUrl + route.description
        let token = ""
        guard let url = URL(string: urlString) else { return nil }

        var urlRequest = URLRequest(url: url)
        //TOKEN
        urlRequest.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")

        if fileData != nil { // FORM-DATA
            guard let mediaImage = Media(withImage: fileData!, forKey: "FileUpload") else { return nil }
            let boundary = UUID().uuidString
            urlRequest.httpMethod = method.rawValue
            urlRequest.setValue("multipart/form-data; boundary=\(boundary)", forHTTPHeaderField: "Content-Type")
            urlRequest.httpBody = createDataBody(withParameters: parameters, media: [mediaImage], boundary: boundary)
        } else { //NORMAL HTTP
            urlRequest.addValue("application/json", forHTTPHeaderField: "Content-Type")
            urlRequest.httpMethod = method.rawValue

            if let params = parameters {
                switch method {
                case .get:
                    var urlComponent = URLComponents(string: urlString)

                    urlComponent?.queryItems = params.map {
                        URLQueryItem(name: $0, value: "\($1)") }
                    urlRequest.url = urlComponent?.url
                case .post, .delete, .patch:
                    let bodyData = try? JSONSerialization.data(withJSONObject: params, options: [])

                    urlRequest.httpBody = bodyData
                }
            }
        }

        return urlRequest
    }
}
