//
//  UIViewController_Extensions.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//
import UIKit

extension UIViewController {
    // return the name of the Controller
    static var getNameasIdentifier: String{
        return String(describing: self)
    }
    // return the instance of ViewController
    static func GetStoryBoardId() -> Self {
        let storyboard = UIStoryboard(name: "Main", bundle: nil)
        return storyboard.instantiateViewController(withIdentifier: getNameasIdentifier) as! Self
    }
    
}
