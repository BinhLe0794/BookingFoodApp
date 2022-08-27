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
    
    public func getValue(for key: String) -> Any? {
        return object(forKey: key)
    }

    public func setValue(for key: String, _ value: Any?) {
        return set(value, forKey: key)
    }
}
