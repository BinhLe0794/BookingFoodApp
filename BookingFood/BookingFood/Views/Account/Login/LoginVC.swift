//
//  LoginVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit
import ProgressHUD
class LoginVC: UIViewController {

    @IBOutlet weak var imgLogo: UIImageView!
    @IBOutlet weak var inputStackView: UIStackView!
    @IBOutlet weak var logoView: UIView!
    @IBOutlet weak var txtUsername: UITextField!
    @IBOutlet weak var txtPassword: UITextField!
    @IBOutlet weak var lbErrorMessage: UILabel!

    @IBOutlet weak var imgTooglePassword: UIImageView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.animationLogo()
        UserDefaults.standard.removeCurrentUser()
        
    }
    
    private func animationLogo() {
        
        let mainWidth = view.frame.width
        
        let sizeLogo = logoView.frame.height/1.4

        imgLogo.frame.size = .init(width: sizeLogo, height: sizeLogo)
        
        imgLogo.frame.origin = .init(x: mainWidth / 2 - self.imgLogo.frame.width / 2, y: 0 - self.imgLogo.frame.height)

        UIView.animate(withDuration: 2) { [self] in
            imgLogo.frame.origin.y = logoView.frame.height/2 - imgLogo.frame.height/2
        }
    }

    @IBAction func togglePassword_Tap(_ sender: Any) {
        txtPassword.isSecureTextEntry = !txtPassword.isSecureTextEntry
        if txtPassword.isSecureTextEntry {
            imgTooglePassword.image = UIImage(systemName: "eye.slash")
        }else {
            imgTooglePassword.image = UIImage(systemName: "eye")
        }
      
    }
    
    @IBAction func btnForget_Clicked(_ sender: Any) {
        ProgressHUD.showError("Let's Contact with the Admin")
    }
    
    @IBAction func btnLogin_Clicked(_ sender: Any) {
        ProgressHUD.show()
        let request: [String: Any] = [
            "username": txtUsername.text?.trimmingCharacters(in: .whitespaces) ?? "",
            "password": txtPassword.text?.trimmingCharacters(in: .whitespaces) ?? ""
        ]
        AuthService.shared.loginRequest(requestBody: request) { apiResult in
            
            switch apiResult {
            case .success(let data):
//                print("data: \(data)")
                //TODO: Save the User Token
                UserDefaults.standard.setCurrentUser(data)
                ProgressHUD.dismiss()
                self.navigationController?.popToRootViewController(animated: true)

            case .failure(let error):
                //print("error: \(error.localizedDescription)")
                DispatchQueue.main.async { [self] in
                    ProgressHUD.dismiss()
                    lbErrorMessage.text = error.localizedDescription
                }

            }
        }
    }
}
