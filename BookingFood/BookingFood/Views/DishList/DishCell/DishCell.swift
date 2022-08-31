//
//  DishCell.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import Kingfisher


class DishCell: UITableViewCell {
    
    static let identifier = String(describing: DishCell.self)

    @IBOutlet weak var imgView: UIImageView!
    
    @IBOutlet weak var lbName: UILabel!
    
    @IBOutlet weak var lbDescription: UILabel!
    @IBOutlet weak var lbPrice: UILabel!
    
    func setup(categoryDishes dish: DishVm) {
        
        imgView.kf.setImage(with: dish.image?.toUrl)
        lbName.text = dish.name
        lbDescription.text = dish.description
        lbPrice.text = dish.formattedPrice
        
    }
}
