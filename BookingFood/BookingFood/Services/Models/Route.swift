//
//  Routes.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import Foundation

enum Route {
    static let baseUrl = "http://localhost:5123"
    
    case login
    case register
    case updateAvatar(String)
    case getHomePage
    case getDishes
    case fetchCategoryById(String)
//    case fetchOrders
    
    var description: String {
        switch self {
        case .login:
            return "/login"
        case .register:
            return "/register"
        case .updateAvatar(let userId):
            return "upload-avatar?fileName=\(userId)"
        case .getHomePage:
            return "/api/dish/categories"
        case .getDishes:
            return "/api/dishes"
        case .fetchCategoryById(let category):
            return "/api/dish?category=\(category)"
        }
    }
}
