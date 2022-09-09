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
    var categories: [DishVm] = []

    var searching = false
    var searchList: [DishVm] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        registerCell()
        self.searchBar.delegate = self
        DishService.shared.fetchCategoriesbyId(categoryName) { [self] apiResult in
            switch apiResult {
            case .success(let data):
                print("data>>>>>>>>: \(data)")
                categories = data
                DispatchQueue.main.async { [self] in
                    tableView.reloadData()
                }
            case .failure(let error):
                print("error: \(error.localizedDescription)")
            }

        }
    }

    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        navigationItem.title = "\(categoryName.uppercased())"
    }
    private func registerCell() {
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)
    }

}

extension CategoryListVC: UISearchBarDelegate {
    func searchBar(_ searchBar: UISearchBar, textDidChange searchText: String) {
        searchList = categories.filter { $0.name.lowercased().prefix(searchText.count) == searchText.lowercased() }
        searching = true
        tableView.reloadData()
    }
    
    func searchBarCancelButtonClicked(_ searchBar: UISearchBar) {
        searching = false
        searchBar.text = ""
        tableView.reloadData()
    }
}

extension CategoryListVC: UITableViewDelegate, UITableViewDataSource {

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        switch searching {
    
        case true:
            return searchList.count
        case false:
            return categories.count
        }
    }


    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: DishCell.identifier, for: indexPath) as! DishCell
        switch searching {
    
        case true:
            cell.setup(categoryDishes: searchList[indexPath.row])
        case false:
            cell.setup(categoryDishes: categories[indexPath.row])
        }
        return cell
    }

    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let dishDetailVC = DishDetailVC.getStoryBoardId()
        dishDetailVC.currentDish = categories[indexPath.row]
        navigationController?.pushViewController(dishDetailVC, animated: true)
    }
}
