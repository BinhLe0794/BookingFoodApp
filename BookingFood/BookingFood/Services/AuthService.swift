//
//  AuthService.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import Foundation
import UIKit

struct UserVm: Codable {
    let fullname, email, avatar, phoneNumber, token,refreshToken: String?
}

struct AuthService {

    static let shared = AuthService()

    private init() { }

    let HttpClientBase = HttpClient.shared

    func loginRequest(requestBody params: [String: Any], completion: @escaping (Result<UserVm, Error>) -> Void) {
        HttpClientBase.makingHttpRequest(route: .login, method: .post, parameters: params, completion: completion)
    }
    func registerRequest(requestBody params: [String: Any], image: UIImage?, completion: @escaping (Result<UserVm, Error>) -> Void) {

        HttpClientBase.makingHttpRequest(route: .register, method: .post, parameters: params, fileImage: image, completion: completion)
    }

}
