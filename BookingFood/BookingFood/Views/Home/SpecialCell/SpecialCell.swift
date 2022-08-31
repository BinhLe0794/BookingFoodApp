//
//  SpecialCell.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import Kingfisher

class SpecialCell: UICollectionViewCell {
    static let identifier = String(describing: SpecialCell.self)
    @IBOutlet weak var imgView: UIImageView!
    
    @IBOutlet weak var lbName: UILabel!
    @IBOutlet weak var lbDescription: UILabel!

    @IBOutlet weak var lbCalories: UILabel!
    @IBOutlet weak var lbPrice: UILabel!
    
    func setup(specialDishes dish: DishVm){
        imgView.kf.setImage(with: dish.image?.toUrl)
        lbName.text = dish.name
        lbDescription.text = dish.description
        lbCalories.text = dish.formattedCalories
        lbPrice.text = dish.formattedPrice
    }
    
}
