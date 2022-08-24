//
//  AccountVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit

class AccountVC: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        print("Account did load")
        let loginController = LoginVC.GetStoryBoardId()
        navigationController?.pushViewController(loginController, animated: true)
//        let sbController = storyboard?.instantiateViewController(withIdentifier: LoginVC.getNameasIdentifier)
//        present(sbController!, animated: true)
    }
    
}
