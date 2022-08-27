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
//    case placeOrder(String)
//    case fetchCategoryDishes(String)
//    case fetchOrders
    
    var description: String {
        switch self {
        case .login:
            return "/login"
        case .register:
            return "/register"
        case .updateAvatar(let userId):
            return "upload-avatar?fileName=\(userId)"
        }
    }
}
