//
//  OrderDetailVC.swift
//  BookingFood
//
//  Created by Huy Binh on 04/09/2022.
//

import UIKit
import ProgressHUD

class OrderDetailVC: UIViewController {

    @IBOutlet weak var tableView: UITableView!
    @IBOutlet weak var lbDate: UILabel!
    @IBOutlet weak var lbTotalCost: UILabel!
    @IBOutlet weak var lbQuantity: UILabel!

    var currentOrder: OrderVm? = nil

    var orderDetails: [OrderDetailVm] = []

    override func viewDidLoad() {
        super.viewDidLoad()

        navigationItem.title = "ORDER DETAIL"
        //tableview
        tableView.delegate = self
        tableView.dataSource = self
        registerCell()
    }

    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        fetchingData()

    }

    private func registerCell() {
        tableView.register(UINib(nibName: OrderDetailCell.identifier, bundle: nil), forCellReuseIdentifier: OrderDetailCell.identifier)
    }
    private func refreshView() {

        let total: Double = orderDetails.reduce(0, { partialResult, orderDetail in
            partialResult + orderDetail.total
        })

        tableView.reloadData()
        lbDate.text = currentOrder?.createdAt ?? "N/A"
        lbQuantity.text = "\(orderDetails.count)"
        lbTotalCost.text = "\(total.toCurrency())"
    }
    private func fetchingData() {
        ProgressHUD.show()
        guard currentOrder != nil else {
            ProgressHUD.showError("The Order is Invalid")
            navigationController?.popViewController(animated: true)
            ProgressHUD.dismiss()
            return
        }

        OrderService.shared.getOrderDetails(currentOrder!.id) { apiResult in
            ProgressHUD.dismiss()
            switch apiResult {
            case .success(let data):
                self.orderDetails = data
                self.refreshView()
            case .failure(let error):
                print("Error>>: \(error.localizedDescription)")
            }
        }

    }

}
extension OrderDetailVC: UITableViewDelegate {

}
extension OrderDetailVC: UITableViewDataSource {
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return orderDetails.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: OrderDetailCell.identifier, for: indexPath) as! OrderDetailCell
        cell.setup(orderDetails[indexPath.row])
        return cell
    }


}
