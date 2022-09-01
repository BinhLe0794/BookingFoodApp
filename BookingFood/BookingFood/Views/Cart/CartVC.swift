//
//  CartVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit

class CartVC: UIViewController {

    @IBOutlet weak var tableView: UITableView!
    
    @IBOutlet weak var lbSubTotal: UILabel!
    
    @IBOutlet weak var lbTotal: UILabel!
    
    var categories:[DishVm] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        registerCell()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        loadCart()
    }
    private func loadCart(){
        guard let localCarts = UserDefaults.standard.getCart() else {return}
        
        categories = localCarts
        
        tableView.reloadData()
        
        updatePrice()
    }
    private func registerCell() {
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)
    }
    private func updatePrice() {
        let total = categories.reduce(0) { partialResult, dish in
            partialResult + dish.price
        }
        DispatchQueue.main.async { [self] in
            lbTotal.text = (total + 2.99).toCurrency()
            lbSubTotal.text = total.toCurrency()
        }
    }
}

extension CartVC: UITableViewDelegate, UITableViewDataSource {
    
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return categories.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: DishCell.identifier, for: indexPath) as! DishCell
        cell.setup(categoryDishes: categories[indexPath.row])
        return cell
    }
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let dishDetailVC = DishDetailVC.getStoryBoardId()
        navigationController?.pushViewController(dishDetailVC, animated: true)
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            tableView.beginUpdates()
            let item = categories.remove(at: indexPath.row)

            if item.cartId != nil {
                UserDefaults.standard.removeCart(item.cartId!)
            }
            tableView.deleteRows(at: [indexPath], with: .fade)
            tableView.endUpdates()
            updatePrice()
        }
    }
    
}
