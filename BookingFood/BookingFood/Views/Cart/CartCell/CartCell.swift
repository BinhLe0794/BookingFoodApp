//
//  CartCell.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit

class CartCell: UITableViewCell {

    static let identifier = String(describing: CartCell.self)
    
    @IBOutlet weak var imgView: UIImageView!
    @IBOutlet weak var lbName: UILabel!
    @IBOutlet weak var lbQuantity: UILabel!
    @IBOutlet weak var lbPrice: UILabel!
    @IBOutlet weak var btnStepper: UIStepper!
    
    func setup(_ dishCart: DishCartVm){
        imgView.kf.setImage(with: dishCart.image?.toUrl)
        lbName.text = dishCart.name
        lbQuantity.text = dishCart.formattedQuantity
        lbPrice.text = dishCart.formattedPrice
        btnStepper.value = Double(dishCart.quantity)
    }
    @IBAction func btnStepper_Clicked(_ sender: Any) {
        lbQuantity.text = "\(Int(btnStepper.value))"
    }
}
