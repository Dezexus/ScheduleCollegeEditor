   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5      Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5      /    )ScheduleCollegeEditor.View.SchoolSubjects   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ScheduleCollegeEditor.View     5      C	viewModel-clr-namespace:ScheduleCollegeEditor.ViewModel     5      $��Stretch=�5	      $��Stretch=�5
      $	��600��5      $	��300��5      $��10q�5        � DataContext  5      =  7ScheduleCollegeEditor.ViewModel.SchoolSubjectsViewModel  5   
    � 	Resources 5         BoolToVisConverter��%   &	        �� 5   
   6'   5      .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��70 �6    � ColumnDefinitions 5   
   �� 5       $	��320��6   $	��350��6.   �� 5!      $	��450��6   $
��auto �6.   .+�5$   
   �� $��Stretch=�6   $��2��6.   $��10q�6?   $��15(�6K     ListBoxBase#���  6Y     
ItemsStyle#���  5%   A   ��5$      �� *SchoolSubjectsList+ �� SelectedItem 5%      �� *SelectedSchoolSubject+6A   � 5'   
   $��2��6    5(      �� 5)      $��30 �6    �� 5*      $��10 �6    �� 5+      $��30 �6    .+�5-      �� $��Удалить��6w   $	��140��6�   $
��10,0q�6�   $
��Left=�6�     
ButtonBase#���  6�   ��6   �� *DeleteSchoolSubjectCommand+��6D   �� *SelectedSchoolSubject+6�   �� 5.      $ ��Редактировать��6u   $	��140��6�   $
��10,0q�6�   $��Right=�6�   #���  6�   ��6   �� *EditSchoolSubjectCommand+��6B   �� *SelectedSchoolSubject+6�   �� 5/      $��2��6   $��Удалить все��6U   $��30��6k   $
��10,0q�6w   #���  6�   ��6"   �� * DeleteAllSchoolSubjectsCommand+6�   50      � 52   
   $��35��6   $��1��6   $	��Top=�6+   $��10q�6C    53      �� 54      $��* �6#   �� 55      $��35 �6#   .+�57      �� 
 � Tag$ 
Поиск��6U   $	��5,0q�6a     TextBoxBase#���  6n   ��6   �� *
FindText+ �� UpdateSourceTrigger$ PropertyChanged=�6n    � 58      $��1��6   $��35��6�   $��Right=�6�   $	��Top=�6�   ��6$   �� *LoadAnimation+y�6F   ��  �� Path$ LoadAnimation� �� 	Converter# [  6�   59      � 5;   
   $��1��6   $��Center=�6   $��Center=�6<   $��10q�6W   $��2��6c    5<      �� 5=      $
��auto �6#    5?      �� 5@      $
��auto �6    �� 5A      $
��auto �6    .+�5D      �� $' !Название предмета��6   $	��350��5E      $��35��6"   $��1��6.   $��10q�6>   #���  6J   ��5D   .   �� $ SchoolSubject.Name�$ PropertyChanged=�.��5E   p   	 J� InputBindings	 5F      �� 5G      

 �� Key$
 Enter��6!    �� Command 6-   �� *AddSchoolSubjectCommand+ �� CommandParameter 6Y   �� *SchoolSubject+.��5H   )   5I      � 5K      $��1��6   $
��10,0q�6    $��35��6.   .+�5L      �� $��Отмена��6r   $	��160��6�   $
��Left=�6�   #���  6�   ��6   �� * CancelEditSchoolSubjectCommand+��6L   �� *
EditMode+6�   �� 5M      $	��160��6�   $��Right=�6�   #���  6�   ��6   �� *AddSchoolSubjectCommand+��6E   �� *SchoolSubject+��6p   �� *NameSaveButton+6�   5N      5O      5R      5T      