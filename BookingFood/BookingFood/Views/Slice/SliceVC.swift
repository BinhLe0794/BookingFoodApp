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
        .init(title: "World-Class Chefs", description: "Our dishes are prepared by only the best.", image: #imageLiteral(resourceName: "slide1")),
        .init(title: "Delicious Dishes", description: "Experience a variety of amazing dishes from different cultures around the world.", image: #imageLiteral(resourceName: "slide3"))
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
        if _currentPage == slices.count - 1 {
            //move to next page
        }else {
            _currentPage += 1
            let indexPath = IndexPath(item: _currentPage, section: 0)
            collectionView.scrollToItem(at: indexPath, at: .centeredHorizontally, animated: true)
        }
    }
    
}

extension SliceVC :  UICollectionViewDelegate,
                     UICollectionViewDelegateFlowLayout,
                     UICollectionViewDataSource {
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return slices.count
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: SliceCVC.identifier, for: indexPath) as! SliceCVC
        
        cell.setup(slices[indexPath.row])
        
        return cell
    }
    
    // set size for each item in the collection
    func collectionView(_ collectionView: UICollectionView,
                        layout collectionViewLayout: UICollectionViewLayout,
                        sizeForItemAt indexPath: IndexPath) -> CGSize {
        
        let size = CGSize(width: collectionView.frame.width,
                         height: collectionView.frame.height)
        
        return size
    }
    
    func scrollViewDidEndDecelerating(_ scrollView: UIScrollView) {
        let width = scrollView.frame.width
        _currentPage = Int(scrollView.contentOffset.x / width)
        pageControl.currentPage = _currentPage
    }
    
}
