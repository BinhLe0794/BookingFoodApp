//
//  OrderVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import ProgressHUD
class OrderVC: UIViewController {

 
    @IBOutlet weak var tableView: UITableView!
    
    @IBOutlet weak var lbOrderCount: UILabel!
    @IBOutlet weak var lbLastOrder: UILabel!
    @IBOutlet weak var lbSpending: UILabel!
    
    var orders: [OrderVm] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tableView.delegate = self
        tableView.dataSource = self
    }
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        if checkAuthentication() {
            fetchingData()
        } else {
            ProgressHUD.showError("Let's login first")
            refreshView()
        }
            
    }
    private func checkAuthentication() -> Bool {
        guard (UserDefaults.standard.getCurrentUser() != nil) else {
            return false
        }
        return true
    }
    private func refreshView() {
        
        let spending = orders.reduce(Double(0)) { partialResult, order in
            partialResult + order.totalCost
        }
        tableView.reloadData()
        lbOrderCount.text = "\(orders.count)"
        lbLastOrder.text = "\(orders.first?.createdAt ?? "N/A")"
        lbSpending.text = "\(spending.toCurrency())"
    }
    private func fetchingData(){
        ProgressHUD.show()
        guard let user = UserDefaults.standard.getCurrentUser() else {return}
        OrderService.shared.getOrders(user.id) { [self] apiResult in
            ProgressHUD.dismiss()
            switch apiResult {
                
            case .success(let data):
                self.orders = data
                refreshView()
            case .failure(let error):
                print("Error: \(error.localizedDescription)")
            }
        }
    }

}

extension OrderVC: UITableViewDelegate {
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let detailController = OrderDetailVC.getStoryBoardId()
        
        detailController.currentOrder = orders[indexPath.row]
        
        self.navigationController?.pushViewController(detailController, animated: true)
        
    }
}

extension OrderVC: UITableViewDataSource {

    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return orders.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: OrderCell.identifier, for: indexPath) as! OrderCell
        
        cell.setup(String(indexPath.row + 1),orders[indexPath.row])

        return cell
    }

}
