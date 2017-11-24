# ButtonsBoxes
Xamarin Forms Rooms List Performance Test Buttons vs Boxes


ButtonsBoxes Data Templates
=============================
Shows the scrolling performance difference on Droid from using Buttons vs Boxes in a ListView. Buttons and Boxes are created within two different cs-based Data Templates. 

Also this sample code offers a reference for one possible way to chain together View Models, trigger bound property UI updates, and send Commands up to the top-level View Model.

Diagram of Classes
===================

RoomsViewModel
- Has a list of RoomViewModels
- Raises a property changed on the TimeSlotViewModel Selected property.

RoomViewModel 
- Has a list a TimeSlotViewModels

TimeSlotViewModel 
- Has a a reference back to its RoomViewModel

TimeSlotButton (UI View)
- Has a BindingContext to a TimeSlotViewModel
- Has a SelectedProperty bound to the TimeSlotViewModel
- Has a Clicked handler which sends a ToggleTimeSlotCommand to the RoomsViewModel, passing the TimeSlotViewModel

App
- Has a property for the RoomsViewModel

Originally based on Xamarin Form Sample Data Template 
========================================================

[Xamarin Form Sample Data Templates](http://github.com/xamarin/xamarin-forms-samples/tree/master/Templates/DataTemplates).



