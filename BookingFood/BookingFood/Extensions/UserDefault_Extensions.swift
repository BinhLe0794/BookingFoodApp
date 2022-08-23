//
//  UserDefault_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import Foundation

extension UserDefaults {
    private enum UserDefaultKeys : String {
        case isHome
    }
    
    var isHome: Bool {
        get{
            bool(forKey: UserDefaultKeys.isHome.rawValue)
        }
        set{
            setValue(newValue, forKey: UserDefaultKeys.isHome.rawValue)
        }
    }
}
