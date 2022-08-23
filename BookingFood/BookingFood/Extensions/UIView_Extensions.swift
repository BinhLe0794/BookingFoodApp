//
//  UIView_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import UIKit

extension UIView {
    
   @IBInspectable var cornerRadius: CGFloat {
        get {
            return self.cornerRadius
        }
        set {
            self.layer.cornerRadius = newValue
        }
    }
}
