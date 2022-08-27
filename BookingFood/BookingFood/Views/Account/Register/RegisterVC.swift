//
//  RegisterVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit
import ProgressHUD

class RegisterVC: UIViewController {

    @IBOutlet weak var imgAvatar: UIImageView!

    @IBOutlet weak var txtUsername: UITextField!

    @IBOutlet weak var txtEmail: UITextField!

    @IBOutlet weak var txtPassword: UITextField!

    @IBOutlet weak var txtPhone: UITextField!
    @IBOutlet weak var txtConfirm: UITextField!

    override func viewDidLoad() {
        super.viewDidLoad()
        imgAvatar.makeRounded()
    }

    @IBAction func chooseImageGallery_Tap(_ sender: Any) {
        let imageGallery = UIImagePickerController()
        imageGallery.delegate = self
        imageGallery.sourceType = .photoLibrary
        imageGallery.allowsEditing = false
        self.present(imageGallery, animated: true)
    }

    private func getRequest() -> [String: String] {
        let request: [String: String] = [
            "Email": txtEmail.text ?? "",
            "Username": txtUsername.text ?? "",
            "PhoneNumber": txtPhone.text ?? "",
            "Password": txtPassword.text ?? "",
            "ConfirmPassword": txtConfirm.text ?? "",
        ]
        return request
    }
    @IBAction func btnRegister_Clicked(_ sender: Any) {

        ProgressHUD.show()
        AuthService.shared.registerRequest(requestBody: getRequest(),
                                           image: imgAvatar.image) { apiResult in

            switch apiResult {
            case .success(let data):
                print("User: \(data.fullname)")
                let loginController = LoginVC.getStoryBoardId()
                ProgressHUD.showSucceed("Register Successfully", interaction: true)
                self.navigationController?.pushViewController(loginController, animated: true)
            case .failure(let error):
                ProgressHUD.showError("\(error.localizedDescription)", image: nil, interaction: true)
                print(error.localizedDescription)
            }
        }
    }


}
extension RegisterVC: UINavigationControllerDelegate, UIImagePickerControllerDelegate {
    func imagePickerController(_ picker: UIImagePickerController, didFinishPickingMediaWithInfo info: [UIImagePickerController.InfoKey: Any]) {
        if let isImage = info[UIImagePickerController.InfoKey(rawValue: UIImagePickerController.InfoKey.originalImage.rawValue)] as? UIImage {
            //Checking the item is Image
            imgAvatar.image = isImage
            ProgressHUD.showSucceed()

        } else {
            //is not a image
            ProgressHUD.showError("Choose Image Failed")
        }
        self.dismiss(animated: true) //
    }
}
