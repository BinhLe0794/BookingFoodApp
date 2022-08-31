//
//  FullDishListVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit

class FullDishListVC: UIViewController {

    @IBOutlet weak var searchBar: UISearchBar!
    
    @IBOutlet weak var tableView: UITableView!
    
//    var categoryName: String = "Burgers"
    var categories:[DishVm] = [
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
    ]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        navigationItem.title = "Dishes"
        // REGISTER TABLE VIEW CELL
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)
    }
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
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
        navigationController?.pushViewController(dishDetailVC, animated: true)
    }
    
}
