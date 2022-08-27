//
//  UIImage_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 25/08/2022.
//

import UIKit
extension UIImageView {
    
    func makeRounded() {
        let radius = self.bounds.width/2.0
        self.layer.cornerRadius = radius
        self.layer.masksToBounds = true
    }
}
