//
//  AlertComponent.swift
//  BookingFood
//
//  Created by Huy Binh on 05/09/2022.
//

import UIKit

struct AlertComponent {
    
//    private init() { }
    
    func AlertInformation(title : String, message:String , callback : ((_ action : UIAlertAction) -> Void)? ) -> UIAlertController{
        
        let alertController = UIAlertController(title: title, message: message, preferredStyle: .alert)
      
        let OKAction = UIAlertAction(title: "Okay", style: .default, handler: callback )
        
        alertController.addAction(OKAction)
        
        return alertController
    }
    
    func AlertConfirmation(title : String, message:String , callback : ((_ action : UIAlertAction) -> Void)?)-> UIAlertController{
        
        let alertController = UIAlertController(title: title, message: message, preferredStyle: .alert)
        // create a cancel action
        let cancelAction = UIAlertAction(title: "Cancel", style: .cancel) { (action) in
            // handle cancel response here. Doing nothing will dismiss the view.
        }
        // add the cancel action to the alertController
        alertController.addAction(cancelAction)

        // create an OK action
        let OKAction = UIAlertAction(title: "OK", style: .destructive,handler: callback)
        // add the OK action to the alert controller
        alertController.addAction(OKAction)
        
        return alertController
    }
}
