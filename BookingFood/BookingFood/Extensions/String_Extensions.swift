//
//  String_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import Foundation

extension String {
    var toUrl : URL? {
        return URL(string: self)
    }
}
