//
//  OrderDetailCell.swift
//  BookingFood
//
//  Created by Huy Binh on 05/09/2022.
//

import UIKit
import Kingfisher

class OrderDetailCell: UITableViewCell {

    static let identifier = String(describing: OrderDetailCell.self)
    
    @IBOutlet weak var imgView: UIImageView!
    @IBOutlet weak var lbName: UILabel!
    @IBOutlet weak var lbQuantity: UILabel!
    @IBOutlet weak var lbTotal: UILabel!
    
    func setup(_ orderDetail: OrderDetailVm) {
        imgView.kf.setImage(with: orderDetail.imageUrl?.toUrl)
        lbName.text = orderDetail.name
        lbQuantity.text = "\(orderDetail.quantity)"
        lbTotal.text = orderDetail.total.toCurrency()
    }
}
