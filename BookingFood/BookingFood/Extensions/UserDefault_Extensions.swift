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

    func getUserToken() -> TokenVm {
        guard let value = object(forKey: UserDefaultKeys.currentUser.rawValue) else {
            return TokenVm(accessToken: "", refreshToken: "")
        }

        let user = try? JSONDecoder().decode(UserVm.self, from: value as! Data)

        guard user != nil else {
            return TokenVm(accessToken: "", refreshToken: "")
        }

        return TokenVm(accessToken: "\(user?.token ?? "")", refreshToken: "\(user?.refreshToken ?? "")")

    }

    func refreshToken(newToken: TokenVm) {
        
        guard var user = getCurrentUser() else {
            return
        }
        user.token = newToken.accessToken
        user.refreshToken = newToken.refreshToken
        
        setCurrentUser(user)
    }

    func removeCurrentUser() {
        removeObject(forKey: UserDefaultKeys.currentUser.rawValue)
    }

    func getCart() -> [DishCartVm]? {
        guard let value = object(forKey: UserDefaultKeys.cartInfo.rawValue) else {
            return nil
        }

        let carts = try? JSONDecoder().decode([DishCartVm]?.self, from: value as! Data)

        guard carts != nil else {
            return nil
        }
        return carts
    }

    func addCart(_ addDish: DishCartVm) {
        let value: Data?

        if var carts = getCart() {
            if let cartIndex = carts.firstIndex(where: { $0.dishId == addDish.dishId }) {
                carts[cartIndex].quantity += addDish.quantity
                if(addDish.price != carts[cartIndex].price) {
                    carts[cartIndex].price = addDish.price
                }
            } else {
                carts.append(addDish)
            }
            //
            value = try? JSONEncoder().encode(carts)
        } else { // the cart is nil
            var newCarts: [DishCartVm] = []
            newCarts.append(addDish)
            value = try? JSONEncoder().encode(newCarts)
        }

        set(value, forKey: UserDefaultKeys.cartInfo.rawValue)
    }
    func removeCart(_ cartId: String) {
        var value: Data?

        if var carts = getCart() {
            guard let removeIndex = carts.firstIndex(where: { $0.cartId == cartId }) else { return }
            carts.remove(at: removeIndex)
            value = try? JSONEncoder().encode(carts)
        }

        guard value != nil else { return }

        set(value, forKey: UserDefaultKeys.cartInfo.rawValue)
    }
    func clearCart() {
        removeObject(forKey: UserDefaultKeys.cartInfo.rawValue)
    }

    public func getValue(for key: String) -> Any? {
        return object(forKey: key)
    }

    public func setValue(for key: String, _ value: Any?) {
        return set(value, forKey: key)
    }
}
