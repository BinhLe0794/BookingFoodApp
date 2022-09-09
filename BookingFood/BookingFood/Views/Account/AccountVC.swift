//
//  AccountVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit
import ProgressHUD
import Kingfisher

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
//        imgAvatar.makeRounded()
    }

    override func viewDidAppear(_ animated: Bool) {

        if !isLogin {
            setViewDefault()
        }
        else {
            let user = UserDefaults.standard.getCurrentUser()
            guard user != nil else { return setViewDefault() }
            setUserView(user!)
            ProgressHUD.showSucceed("Authentication", interaction: true)
        }
    }

    @IBAction func btnSignIn_Clicked(_ sender: Any) {
        if isLogin {
            logoutServer()
        }
        pushToLogin()
    }

    private func logoutServer() {

        let result = AlertComponent().AlertConfirmation(title: "Information", message: "Sign Out this account") { action in
            
            let token = UserDefaults.standard.getUserToken()
            
            AuthService.shared.logoutRequest(token) { apiResult in
                switch apiResult {
                    
                case .success(_):
                    print("Logut Success")
                case .failure(_):
                    print("Logout Failed")
                }
            }
            UserDefaults.standard.removeCurrentUser()
            self.pushToLogin()
        }
            self.present(result, animated: true)
            
        }

        private func pushToLogin() {
            let loginController = LoginVC.getStoryBoardId()
            navigationController?.modalPresentationStyle = .fullScreen
            navigationController?.pushViewController(loginController, animated: true)
        }



        private func setViewDefault() {
            lbName.text = "Guest"
            lbEmail.text = "No Email"
            lbPhone.text = "N/A"
            lbOrder.text = "0"
            lbLastOrder.text = "N/A"
            imgAvatar.image = UIImage(named: "logo")

            btnSignIn.setTitle("Let's Sign In", for: .normal)
            btnSignIn.setTitleColor(.white, for: .normal)
            btnSignIn.backgroundColor = .black

            lbOr.isHidden = false
            btnRegister.isHidden = false
        }
        private func setUserView(_ user: UserVm) {
            guard user.token != nil else {
                setViewDefault()
                return
            }

            lbName.text = "\(user.fullname)"
            lbEmail.text = "\(user.email)"
            lbPhone.text = "\(user.phoneNumber)"
            lbOrder.text = "0"
            lbLastOrder.text = "\(user.lastLogin ?? "N/A")"
           
            if let avatar = user.avatar?.formattedUrl {
                imgAvatar.kf.setImage(with: avatar)
            } else {
                imgAvatar.image = UIImage(named: "logo")
            }

            lbOr.isHidden = true
            btnRegister.isHidden = true
            btnSignIn.setTitle("Logout", for: .normal)
            btnSignIn.setTitleColor(.red, for: .normal)
            btnSignIn.backgroundColor = .darkGray
        }
    }
