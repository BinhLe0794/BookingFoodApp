//
//  LoginVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit

class LoginVC: UIViewController {

    @IBOutlet weak var imgLogo: UIImageView!
    @IBOutlet weak var inputStackView: UIStackView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.animationLogo()
    }
    private func animationLogo(){
        let mainWidth = view.frame.width
//        let mainHeight = view.frame.height
        let sizeLogo = inputStackView.frame.origin.y/2
        print("sizeLogo: \(sizeLogo)")
        imgLogo.frame.size = .init(width: sizeLogo, height: sizeLogo)
        imgLogo.frame.origin = .init(x: mainWidth/2 - self.imgLogo.frame.width/2, y: 0 - self.imgLogo.frame.height)
        
        UIView.animate(withDuration: 2) { [self] in
            imgLogo.frame.origin.y = inputStackView.frame.origin.y - imgLogo.frame.size.height*1.5
        }
    }
}
