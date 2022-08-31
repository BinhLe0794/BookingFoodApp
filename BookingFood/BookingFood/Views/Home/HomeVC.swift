//
//  HomeVC.swift
//  BookingFood
//
//  Created by Huy Binh on 24/08/2022.
//

import UIKit

class HomeVC: UIViewController {

    @IBOutlet weak var collectionCategories: UICollectionView!

    @IBOutlet weak var collectionPopular: UICollectionView!

    @IBOutlet weak var collectionSpecial: UICollectionView!


    var categories: [CategoryVm] = [
            .init(name: "Burgers", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"),
            .init(name: "Burgers", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"),
            .init(name: "Burgers", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"),
            .init(name: "Burgers", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"),
            .init(name: "Burgers", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"),
            .init(name: "All", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80")
    ]
    var populars: [DishVm] = [
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
    ]
    var special: [DishVm] = [
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
            .init(id: "1", name: "Burgers", description: "A hamburger is a sandwich consisting of a cooked meat patty on a bun or roll.", image: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", category: "1", calories: 120, price: 255),
    ]

    override func viewDidLoad() {
        super.viewDidLoad()
        registerCell();

    }

    private func registerCell() {
        collectionCategories.register(UINib(nibName: CategoryCell.identifier, bundle: nil), forCellWithReuseIdentifier: CategoryCell.identifier)

        collectionPopular.register(UINib(nibName: PopularCell.identifier, bundle: nil), forCellWithReuseIdentifier: PopularCell.identifier)

        collectionSpecial.register(UINib(nibName: SpecialCell.identifier, bundle: nil), forCellWithReuseIdentifier: SpecialCell.identifier)
    }
}

extension HomeVC: UICollectionViewDelegate, UICollectionViewDataSource {
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        switch collectionView {
        case collectionCategories:
            return categories.count
        case collectionPopular:
            return populars.count
        case collectionSpecial:
            return special.count
        default: return 0
        }
    }

    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {

        switch collectionView {
        case collectionCategories:
            let cell = collectionCategories.dequeueReusableCell(withReuseIdentifier: CategoryCell.identifier, for: indexPath) as! CategoryCell

            cell.setup(categories[indexPath.row])
            return cell
        case collectionPopular:
            let cell = collectionPopular.dequeueReusableCell(withReuseIdentifier: PopularCell.identifier, for: indexPath) as! PopularCell

            cell.setup(populars[indexPath.row])
            return cell
        case collectionSpecial:
            let cell = collectionSpecial.dequeueReusableCell(withReuseIdentifier: SpecialCell.identifier, for: indexPath) as! SpecialCell

            cell.setup(specialDishes: special[indexPath.row])
            return cell
        default:
            return UICollectionViewCell()
        }
    }

    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        switch collectionView {
        case collectionCategories:

            print(categories[indexPath.row].name)
            switch categories[indexPath.row].name {
            case "All":
                let fullDishListVC = FullDishListVC.getStoryBoardId()
                navigationController?.pushViewController(fullDishListVC, animated: true)
            default:
                let categoryListVC = CategoryListVC.getStoryBoardId()
                navigationController?.pushViewController(categoryListVC, animated: true)

            }

        case collectionPopular:
            let dishDetailVC = DishDetailVC.getStoryBoardId()
            print(categories[indexPath.row].name)
            navigationController?.pushViewController(dishDetailVC, animated: true)
        case collectionSpecial:
            let dishDetailVC = DishDetailVC.getStoryBoardId()
            print(categories[indexPath.row].name)
            navigationController?.pushViewController(dishDetailVC, animated: true)
        default: print("")
        }
    }
}
