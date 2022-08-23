//
//  SliceVC.swift
//  BookingFood
//
//  Created by Huy Binh on 23/08/2022.
//

import UIKit

class SliceVC: UIViewController {

    
    @IBOutlet weak var collectionView: UICollectionView!
    @IBOutlet weak var btnNext: UIButton!
    @IBOutlet weak var pageControl: UIPageControl!
    
    var slices: [SliceVm] = [
        .init(title: "Delicious Dishes", description: "Experience a variety of amazing dishes from different cultures around the world.", image: #imageLiteral(resourceName: "slide2")),
        .init(title: "World-Class Chefs", description: "Our dishes are prepared by only the best.", image: #imageLiteral(resourceName: "slide1"))
    ]
    
    var _currentPage = 0 {
        didSet {
            if _currentPage == slices.count - 1 {
                btnNext.setTitle("Get Started", for: .normal)
            }else {
                btnNext.setTitle("Next", for: .normal)
            }
        }
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        
        collectionView.dataSource = self
        collectionView.delegate = self
        pageControl.numberOfPages = slices.count
    }
    
    @IBAction func btnNext_Clicked(_ sender: Any) {
        pageControl.currentPage = _currentPage
    }
    
}

extension SliceVC :  UICollectionViewDelegate, UICollectionViewDataSource {
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return slices.count
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: SliceCVC.identifier, for: indexPath) as! SliceCVC
        
        cell.setup(slices[indexPath.row])
        
        return cell
    }
    
    
}
