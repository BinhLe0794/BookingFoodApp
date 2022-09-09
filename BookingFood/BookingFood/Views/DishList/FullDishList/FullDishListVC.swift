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
    var categories:[DishVm] = []
    
    var searching = false
    var searchList: [DishVm] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        navigationItem.title = "All Dishes"
        self.searchBar.delegate = self
        registerCell()
    }
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        fetchingData()
    }
    
    private func registerCell(){
        tableView.register(UINib(nibName: DishCell.identifier, bundle: nil), forCellReuseIdentifier: DishCell.identifier)
    }
    private func fetchingData() {
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

}
extension FullDishListVC: UISearchBarDelegate {
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

extension FullDishListVC: UITableViewDelegate, UITableViewDataSource {
    
    
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
