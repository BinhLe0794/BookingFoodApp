//
//  UserDefault_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import Foundation

enum UserDefaultKeys: String {
    case isHome
    case currentUser
    case cartInfo
}

extension UserDefaults {

    var isHome: Bool {
        get {
            bool(forKey: UserDefaultKeys.isHome.rawValue)
        }
        set {
            setValue(newValue, forKey: UserDefaultKeys.isHome.rawValue)
        }
    }
    func getCurrentUser() -> UserVm? {
        guard let value = object(forKey: UserDefaultKeys.currentUser.rawValue) else {
            return nil
        }
        
        let user = try? JSONDecoder().decode(UserVm.self, from: value as! Data)
        return user
    }
    func setCurrentUser(_ user: UserVm) {
        let value = try? JSONEncoder().encode(user)

        set(value, forKey: UserDefaultKeys.currentUser.rawValue)
    }

    func removeCurrentUser() {
        removeObject(forKey: UserDefaultKeys.currentUser.rawValue)
    }
    
    func getCart() -> [DishVm]? {
        guard let value = object(forKey: UserDefaultKeys.cartInfo.rawValue) else {
            return nil
        }
        
        let carts = try? JSONDecoder().decode([DishVm]?.self, from: value as! Data)
        
        guard carts != nil else {
            return nil
        }
        return carts
    }
    
    func addCart(_ dish: DishVm) {
        let value: Data?
        
        if var carts = getCart() {
            carts.append(dish)
            value = try? JSONEncoder().encode(carts)
        }else {
            var newCarts: [DishVm] = []
            newCarts.append(dish)
            value = try? JSONEncoder().encode(newCarts)
        }

        set(value, forKey: UserDefaultKeys.cartInfo.rawValue)
    }
    func removeCart(_ cartId: String) {
        var value: Data?
        
        if var carts = getCart() {
            guard let removeIndex = carts.firstIndex(where: { $0.cartId == cartId} ) else {return}
            carts.remove(at: removeIndex)
            value = try? JSONEncoder().encode(carts)
        }
        
        guard value != nil else {return}
        
        set(value, forKey: UserDefaultKeys.cartInfo.rawValue)
    }
    
    
    public func getValue(for key: String) -> Any? {
        return object(forKey: key)
    }

    public func setValue(for key: String, _ value: Any?) {
        return set(value, forKey: key)
    }
}
