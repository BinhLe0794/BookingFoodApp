//
//  DishVm.swift
//  BookingFood
//
//  Created by Huy Binh on 30/08/2022.
//

import Foundation

struct DishVm : Decodable {
    let id,name: String
    let description,image,category: String?
    let calories: Int?
    let price: Double
    
    var formattedCalories: String {
        return "\(calories ?? 0) calories"
    }
    
    var formattedPrice: String {
        return price.toCurrency()
    }
}
