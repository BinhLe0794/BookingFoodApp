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
    case logout
    case refreshToken

    case getHomePage
    case getDishes
    case fetchCategoryById(String)

    case checkout
    case getOrders(String)
    case getOrderDetails(String)

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
        case .checkout:
            return "/api/order/check-out"
        case .getOrders(let userId):
            return "/api/Order?userId=\(userId)"
        case .getOrderDetails(let orderId):
            return "/api/Order/\(orderId)"

        case .logout:
            return "/logout"
        case .refreshToken:
            return "/refresh-token"
        }
    }
}
