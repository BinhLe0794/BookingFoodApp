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
    
    var formattedUrl: URL? {
        return URL(string: "https://\(self.replaceCharacters(characters: "\\", toSeparator: "/"))")
        
    }
    func replaceCharacters(characters: String, toSeparator: String) -> String {
        let characterSet = CharacterSet(charactersIn: characters)
        let components = components(separatedBy: characterSet)
        let result = components.joined(separator: toSeparator)
        return result
    }

    func wipeCharacters(characters: String) -> String {
        return self.replaceCharacters(characters: characters, toSeparator: "")
    }
}

extension Double {
    func toCurrency() -> String {
        let formatter = NumberFormatter()
        formatter.numberStyle = .currency
        formatter.locale = Locale(identifier: "en_US")
        return formatter.string(from: self as NSNumber)!
    }
}
