//
//  OrderService.swift
//  BookingFood
//
//  Created by Huy Binh on 03/09/2022.
//

import Foundation

struct DishCartVm: Codable {
    let cartId,dishId,name: String
    var quantity: Int
    var price: Double
    let image: String?
    
    var formattedPrice: String {
        return "\(price.toCurrency())"
    }
    var formattedQuantity: String {
        return "\(quantity)"
    }
}

struct CheckoutRequest: Encodable {
    let userId: String
    let details: [DishCartVm]
}
struct OrderDetailVm: Codable {
    let id: String
    let category,name,imageUrl: String?
    let quantity: Int
    let price,total: Double
}
struct OrderVm: Codable {
    let id: String
    let orderDetails: [OrderDetailVm]?
    let quantity: Int
    let totalCost: Double
    let createdAt: String?
}

struct OrderService {
    
    static let shared = OrderService()

    private init() { }

    let HttpClientBase = HttpClient.shared
    
    func checkoutCart(_ params: CheckoutRequest,completion: @escaping (Result<Bool, Error>) -> Void) {
              
        let json = try? JSONEncoder().encode(params)

        HttpClientBase.makingHttpRequest(route: .checkout, method: .post,bodyData: json,parameters: nil, completion: completion)
    }
    
    func getOrders(_ userId: String, completion: @escaping (Result<[OrderVm], Error>) -> Void) {
        
        HttpClientBase.makingHttpRequest(route: .getOrders(userId), method: .get, completion: completion)
    }
    
    func getOrderDetails(_ orderId: String, completion: @escaping (Result<[OrderDetailVm], Error>) -> Void) {
        
        HttpClientBase.makingHttpRequest(route: .getOrderDetails(orderId), method: .get, completion: completion)
    }
    
}
