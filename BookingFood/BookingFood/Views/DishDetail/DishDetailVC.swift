//
//  DishDetailVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import ProgressHUD

class DishDetailVC: UIViewController {

    
    @IBOutlet weak var imgView: UIImageView!
    @IBOutlet weak var btnAdd: UIButton!
    @IBOutlet weak var btnStepper: UIStepper!
    @IBOutlet weak var lbQuantity: UILabel!
    @IBOutlet weak var lbDescription: UILabel!
    @IBOutlet weak var lbCalories: UILabel!
    @IBOutlet weak var lbPrice: UILabel!
    @IBOutlet weak var lbName: UILabel!
    
    var currentDish: DishVm!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        loadData()
    }
    private func loadData() {
        imgView.kf.setImage(with: currentDish.image?.toUrl)
        lbName.text = currentDish.name
        lbPrice.text = currentDish.formattedPrice
        lbDescription.text = currentDish.description
        lbCalories.text = currentDish.formattedCalories
        lbQuantity.text = "\(btnStepper.value)"
    }
    
    @IBAction func btnStepper_Clicked(_ sender: Any) {
        lbQuantity.text = "\(btnStepper.value)"
    }
    
    @IBAction func btnAdd_Clicked(_ sender: Any) {
        
        let count = Int(btnStepper.value)
        
        let addDish = DishCartVm(cartId: UUID().uuidString, dishId: currentDish.id, name: currentDish.name
                                 , quantity: count, price: currentDish.price, image: currentDish.image)
        UserDefaults.standard.addCart(addDish)
        
        ProgressHUD.showSuccess()
    }
    
}
