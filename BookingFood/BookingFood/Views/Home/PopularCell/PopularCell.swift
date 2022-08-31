//
//  PopularCell.swift
//  BookingFood
//
//  Created by Huy Binh on 30/08/2022.
//

import UIKit
import Kingfisher

class PopularCell: UICollectionViewCell {
    @IBOutlet weak var lbName: UILabel!
    
    @IBOutlet weak var imgView: UIImageView!
    
    @IBOutlet weak var lbCalories: UILabel!
    
    @IBOutlet weak var lbDescription: UILabel!
    
    @IBOutlet weak var lbPrice: UILabel!
    
    static let identifier = String(describing: PopularCell.self)

    func setup(_ dish: DishVm){
        
        lbName.text = dish.name
        imgView.kf.setImage(with: dish.image?.toUrl)
        lbCalories.text = dish.formattedCalories
        lbPrice.text = dish.formattedPrice
        lbDescription.text = dish.description
    }
    
}
