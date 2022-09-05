//
//  HttpModels.swift
//  BookingFood
//
//  Created by Huy Binh on 26/08/2022.
//

import Foundation
import UIKit

enum Method: String {
    case get = "GET"
    case post = "POST"
    case delete = "DELETE"
    case patch = "PATCH"
}

enum AppError: LocalizedError {

    case errorDecoding
    case unknownError
    case invalidUrl
    case serverError(String)
    case Unauthorized

    var errorDescription: String? {
        switch self {

        case .errorDecoding:
            return "Response could not be decoded"
        case .unknownError:
            return "Ohhs Somethings wrong"
        case .invalidUrl:
            return "The Url is invalid"
        case .serverError(let error):
            return error
        case .Unauthorized:
            return "Un-Authorized"
        }
    }
}

struct Media {
    let key: String
    let filename: String
    let data: Data
    let mimeType: String

    init?(withImage image: UIImage, forKey key: String) {
        self.key = key
        self.mimeType = "image/jpeg"
        self.filename = "kyleleeheadiconimage234567.jpg"

        guard let data = image.jpegData(compressionQuality: 0.7) else { return nil }
        self.data = data
    }

}
