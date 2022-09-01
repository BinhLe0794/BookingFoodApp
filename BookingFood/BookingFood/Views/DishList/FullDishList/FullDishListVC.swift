//
//  FullDishListVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit
import ProgressHUD

class FullDishListVC: UIViewController {

    @IBOutlet weak var searchBar: UISearchBar!
    
    @IBOutlet weak var tableView: UITableView!
    
//    var categoryName: String = "Burgers"
    var categories:[DishVm] = [
//        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", category: "1", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", calories: 120, price: 255),
    ]

    override func viewDidLoad() {
        super.viewDidLoad()
        navigationItem.title = "All Dishes"
        registerCell()
        ProgressHUD.show()
        DishService.shared.fetchDishes { [self] apiResult in
            ProgressHUD.dismiss()
            switch apiResult {
            case .success(let data):
                categories = data
                DispatchQueue.main.async { [self] in
                    tableView.reloadData()
                }
            case .failure(let error):
                print("\(error.localizedDescription)")
            }
        }
    }
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
    }
    
    private func registerCell(){
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)

    }

}
extension FullDishListVC: UITableViewDelegate, UITableViewDataSource {
    
    
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
        dishDetailVC.currentDish = categories[indexPath.row]
        navigationController?.pushViewController(dishDetailVC, animated: true)
    }
    
}
