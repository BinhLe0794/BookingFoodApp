//
//  OrderCell.swift
//  BookingFood
//
//  Created by Huy Binh on 04/09/2022.
//

import UIKit

class OrderCell: UITableViewCell {
    
    static let identifier = String(describing: OrderCell.self)
    
    @IBOutlet weak var lbTotalCost: UILabel!
    @IBOutlet weak var lbQuantity: UILabel!
    @IBOutlet weak var lbCreatedAt: UILabel!
    @IBOutlet weak var lbNumber: UILabel!
    
    @IBOutlet weak var viewCell: UIView!
    func setup(_ indexPath: String, _ order: OrderVm) {
        
        lbNumber.text = indexPath
        lbCreatedAt.text = order.createdAt
        lbQuantity.text = "\(order.quantity)"
        lbTotalCost.text = order.totalCost.toCurrency()
    }
}
