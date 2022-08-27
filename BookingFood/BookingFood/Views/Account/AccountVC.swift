//
//  AccountVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit

class AccountVC: UIViewController {


    @IBOutlet weak var imgAvatar: UIImageView!
    @IBOutlet weak var lbName: UILabel!
    @IBOutlet weak var lbEmail: UILabel!

    @IBOutlet weak var lbLastOrder: UILabel!
    @IBOutlet weak var lbPhone: UILabel!
    @IBOutlet weak var lbOrder: UILabel!

    @IBOutlet weak var btnSignIn: UIButton!
    @IBOutlet weak var btnRegister: UIButton!
    @IBOutlet weak var lbOr: UILabel!
    var isLogin: Bool {
        get {
            let user = UserDefaults.standard.getCurrentUser()
            if (user?.token != nil) {
                return true
            }
            return false
        }
    }

    override func viewDidLoad() {
        super.viewDidLoad()
//        let sbController = storyboard?.instantiateViewController(withIdentifier: LoginVC.getNameasIdentifier)
//        present(sbController!, animated: true)
        if !isLogin {
            pushToLogin()
        }
    }

    override func viewDidAppear(_ animated: Bool) {
        if !isLogin {
            setViewDefault()
        }
        else {
            let user = UserDefaults.standard.getCurrentUser()
            guard user != nil else { return setViewDefault() }
            setUserView(user!)
        }
    }

    @IBAction func btnSignIn_Clicked(_ sender: Any) {
        if isLogin {
            print("Logout")
            UserDefaults.standard.removeCurrentUser()
        }
        pushToLogin()
    }


    private func pushToLogin() {
        let loginController = LoginVC.getStoryBoardId()
        navigationController?.modalPresentationStyle = .fullScreen
        navigationController?.pushViewController(loginController, animated: true)
    }
    func setViewDefault() {
        lbName.text = "Name: Guest"
        lbEmail.text = "Email: No Email"
        lbPhone.text = "Phone: N/A"
        lbOrder.text = "Order: 0"
        lbLastOrder.text = "Last Order: N/A"
        
        btnSignIn.setTitle("Let's Sign In", for: .normal)
        btnSignIn.setTitleColor(.white, for: .normal)
        btnSignIn.backgroundColor = .black
        
        lbOr.isHidden = false
        btnRegister.isHidden = false
    }
    func setUserView(_ user: UserVm) {
        lbName.text = "Name: \(user.fullname ?? "\("Guest")")"
        lbEmail.text = "Email: \(user.email ?? "\("No Email")")"
        lbPhone.text = "Phone: \(user.phoneNumber ?? "\("N/A")")"
        lbOrder.text = "Order: 0"
        lbLastOrder.text = "Last Order: N/A"

        lbOr.isHidden = true
        btnRegister.isHidden = true
        btnSignIn.setTitle("Logout", for: .normal)
        btnSignIn.setTitleColor(.red, for: .normal)
        btnSignIn.backgroundColor = .lightGray
    }
}
