//
//  CartVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import ProgressHUD

class CartVC: UIViewController {

    @IBOutlet weak var tableView: UITableView!

    @IBOutlet weak var lbSubTotal: UILabel!

    @IBOutlet weak var lbTotal: UILabel!

    var dishCarts: [DishCartVm] = []

    override func viewDidLoad() {
        super.viewDidLoad()
        defaultInfo()
        registerCell()
    }

    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        loadCart()
    }
    private func defaultInfo() {
        lbSubTotal.text = "0.00$"
        lbTotal.text = "2.99$"
    }
    private func loadCart() {
        guard let localCarts = UserDefaults.standard.getCart() else { return }

        dishCarts = localCarts

        tableView.reloadData()

        updatePrice()
    }
    private func registerCell() {
        tableView.register(UINib(nibName: CartCell.identifier, bundle: nil), forCellReuseIdentifier: CartCell.identifier)
    }
    private func updatePrice() {
        let total = dishCarts.reduce(0) { partialResult, dish in
            partialResult + (dish.price * Double(dish.quantity))
        }
        DispatchQueue.main.async { [self] in
            lbTotal.text = (total + 2.99).toCurrency()
            lbSubTotal.text = total.toCurrency()
        }
    }


    @IBAction func btn_Checkout_Clicked(_ sender: Any) {
        
        guard let user = UserDefaults.standard.getCurrentUser() else {
            ProgressHUD.showError("Let's login")
            return
        }
        
        loadCart()
        
        let request = CheckoutRequest(userId: user.id, details: dishCarts)
        print(request)
        OrderService.shared.checkoutCart(request) { apiResult in
            switch apiResult {
            case .success(_):
                ProgressHUD.showSuccess()
                UserDefaults.standard.clearCart()
                self.dismiss(animated: true)
            case .failure(let error):
                ProgressHUD.showError(error.localizedDescription)
                print("\(error.localizedDescription)")
            }
        }

    }
}

extension CartVC: UITableViewDelegate, UITableViewDataSource {


    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return dishCarts.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {

        let cell = tableView.dequeueReusableCell(withIdentifier: CartCell.identifier, for: indexPath) as! CartCell

        cell.setup(dishCarts[indexPath.row])

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
            let item = dishCarts.remove(at: indexPath.row)
            //remove localStorage
            UserDefaults.standard.removeCart(item.cartId)

            tableView.deleteRows(at: [indexPath], with: .fade)
            tableView.endUpdates()
            updatePrice()
        }
    }

}
