   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5      Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5      '    !ScheduleCollegeEditor.View.Groups   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ScheduleCollegeEditor.View     5      C	viewModel-clr-namespace:ScheduleCollegeEditor.ViewModel     5      $��Stretch=�5	      $��Stretch=�5
      $	��600��5      $	��300��5      $��10q�5        � DataContext  5      5  /ScheduleCollegeEditor.ViewModel.GroupsViewModel  5   
    � 	Resources 5         BoolToVisConverter��%   &	        �� 5   
   6'   5      .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��70 �6    � ColumnDefinitions 5    
   �� 5!      $	��320��6   $	��350��6.   �� 5"      $	��450��6   $
��auto �6.   .+�5%   
   �� $��Stretch=�6   $��2��6.   $��10q�6?   $��15(�6K     ListBoxBase#���  6Y     
ItemsStyle#���  5&   9   ��5%      �� *
GroupsList+ �� SelectedItem 5&      �� *SelectedGroup+69   � 5(   
   $��2��6    5)      �� 5*      $��30 �6    �� 5+      $��10 �6    �� 5,      $��30 �6    .+�5.      �� $��Удалить��6g   $	��140��6y   $
��10,0q�6�   $
��Left=�6�     
ButtonBase#���  6�   ��6   �� *DeleteGroupCommand+��6<   �� *SelectedGroup+6�   �� 5/      $ ��Редактировать��6e   $	��140��6}   $
��10,0q�6�   $��Right=�6�   #���  6�   ��6   �� *EditGroupCommand+��6:   �� *SelectedGroup+6�   �� 50      $��2��6   $��Удалить все��6L   $��30��6b   $
��10,0q�6n   #���  6|   ��6"   �� *DeleteAllGroupCommand+6|   51      � 53   
   $��35��6   $��1��6   $	��Top=�6+   $��10q�6C    54      �� 55      $��* �6#   �� 56      $��35 �6#   .+�58      �� 
 � Tag$ 
Поиск��6U   $	��5,0q�6a     TextBoxBase#���  6n   ��6   �� *
FindText+ �� UpdateSourceTrigger$ PropertyChanged=�6n    � 59      $��1��6   $��35��6�   $��Right=�6�   $	��Top=�6�   ��6$   �� *LoadAnimation+y�6F   ��  �� Path$ LoadAnimation� �� 	Converter# [  6�   5:      � 5<   
   $��1��6   $��Center=�6   $��Center=�6<   $��10q�6W   $��2��6c    5=      �� 5>      $
��auto �6#    5@      �� 5A      $
��auto �6    �� 5B      $
��auto �6    .+�5E      �� $# Название группы��6   $	��350��5F      $��35��6&   $��1��62   $��10q�6B   #���  6N   ��5E   ,   �� $ 
Group.Name�$ PropertyChanged=�.��5F   t   	 J� InputBindings	 5G      �� 5H      

 �� Key$
 Enter��6!    �� Command 6-   �� *AddGroupCommand+ �� CommandParameter 6Q   �� *Group+.��5I   )   5J      � 5L      $��1��6   $
��10,0q�6    $��35��6.   .+�5M      �� $��Отмена��6�   $	��160��6�   $
��Left=�6�   #���  6�   ��6   �� *CancelEditGroupCommand+��6D   �� *
EditMode+$ PropertyChanged=�6�   �� 5N      $	��160��6�   $��Right=�6�   #���  6�   ��6   �� *AddGroupCommand+��6=   �� *Group+��6`   �� *NameSaveButton+6�   5O      5P      5R      5S      