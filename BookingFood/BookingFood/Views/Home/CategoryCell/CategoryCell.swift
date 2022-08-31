//
//  CategoryCell.swift
//  BookingFood
//
//  Created by Huy Binh on 30/08/2022.
//

import UIKit
import Kingfisher

class CategoryCell: UICollectionViewCell {

    static let identifier = String(describing: CategoryCell.self)
    
    @IBOutlet weak var imgCategory: UIImageView!
    
    @IBOutlet weak var lbName: UILabel!
    
    func setup(_ category: CategoryVm){
        lbName.text = category.name
        imgCategory.kf.setImage(with: category.image.toUrl)
    }
}
