//
//  SliceCVC.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import UIKit

class SliceCVC: UICollectionViewCell {
    
    @IBOutlet weak var imageView: UIImageView!
    @IBOutlet weak var lbTitle: UILabel!
    @IBOutlet weak var lbDescription: UILabel!
    
    static let identifier = String(describing: SliceCVC.self)
    
    func setup(_ slice: SliceVm){
        imageView.image = slice.image
        lbTitle.text = slice.title
        lbDescription.text = slice.description
    }
}
