//
//  AuthService.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import Foundation
import UIKit

struct UserVm: Codable {
    let id,fullname,email,phoneNumber: String
    var avatar, token, refreshToken: String?
}
struct TokenVm: Codable {
    let accessToken,refreshToken: String
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
    func logoutRequest(_ token: TokenVm ,completion: @escaping (Result<Bool, Error>) -> Void) {
        
        let json = try? JSONEncoder().encode(token)
        
        HttpClientBase.makingHttpRequest(route: .logout, method: .post,bodyData: json, completion: completion)
    }
    
    func refreshTokenRequest(_ token: TokenVm ,completion: @escaping (Result<TokenVm, Error>) -> Void) {
        
        let json = try? JSONEncoder().encode(token)
        
        HttpClientBase.makingHttpRequest(route: .refreshToken, method: .post,bodyData: json, completion: completion)
    }
}
