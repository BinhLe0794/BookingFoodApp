//
//  DishService.swift
//  BookingFood
//
//  Created by Huy Binh on 01/09/2022.
//

import Foundation

struct HomepageVm: Decodable {
    let categories: [CategoryVm]?
    let populars,specials: [DishVm]?
}
struct DishService {
    static let shared = DishService()

    private init() { }

    let HttpClientBase = HttpClient.shared
    
    func fetchCategories(completion: @escaping (Result<HomepageVm, Error>) -> Void) {
        
        HttpClientBase.makingHttpRequest(route: .getHomePage, method: .get, parameters: nil, completion: completion)
    }
    func fetchCategoriesbyId(_ category: String ,completion: @escaping (Result<[ DishVm], Error>) -> Void) {
        
        HttpClientBase.makingHttpRequest(route: .fetchCategoryById(category) , method: .get, parameters: nil, completion: completion)
    }
    func fetchDishes(completion: @escaping (Result<[ DishVm], Error>) -> Void) {
        
        HttpClientBase.makingHttpRequest(route: .getDishes, method: .get, parameters: nil, completion: completion)
    }
}
