//
//  CategoryListVC.swift
//  BookingFood
//
//  Created by Huy Binh on 31/08/2022.
//

import UIKit

class CategoryListVC: UIViewController {

    @IBOutlet weak var tableView: UITableView!
    
    @IBOutlet var searchBar: UISearchBar!
    var categoryName: String = "Burgers"
    var categories:[DishVm] = [
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
        .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
    ]
    
    override func viewDidLoad() {
        super.viewDidLoad()
//        navigationItem.titleView = searchBar
        registerCell()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        navigationItem.title = "\(categoryName.uppercased())"
    }
    private func registerCell(){
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)
    }

}

extension CategoryListVC: UITableViewDelegate,UITableViewDataSource {
    
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
