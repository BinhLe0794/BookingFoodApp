//
//  DishVm.swift
//  BookingFood
//
//  Created by Huy Binh on 30/08/2022.
//

import Foundation

struct DishVm : Codable {
    
    let id,name: String
    let description,category,image: String?
    let calories: Int?
    let price: Double
    
    var cartId: String?
    
    var formattedCalories: String {
        return "\(calories ?? 0) calories"
    }
    
    var formattedPrice: String {
        return price.toCurrency()
    }
    
}
