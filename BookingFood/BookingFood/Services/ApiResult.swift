//
//  ApiResult.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import Foundation
struct ApiResult<T: Decodable> : Decodable {
    let isSuccessed: Bool
    let message: String?
    let resultObj: T?
}
