   M S B A M L   `   `   `  ���� P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  /    )ScheduleCollegeEditor.View.SchoolSubjects   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:ScheduleCollegeEditor.View     C	viewModel-clr-namespace:ScheduleCollegeEditor.ViewModel     $��Stretch=�$��Stretch=�$	��600��$	��300��$��10q�  � DataContext  =  7ScheduleCollegeEditor.ViewModel.SchoolSubjectsViewModel   � 	Resources    BoolToVisConverter��%   &	        �� .��� $��Center=� � RowDefinitions �� $��* ��� $��* ��� $��70 � � ColumnDefinitions �� $	��320��$	��350���� $	��450��$
��auto �.+��� $��Stretch=�$��2��$��10q�$��15(�  ListBoxBase#���    
ItemsStyle#���  ���� *SchoolSubjectsList+ �� SelectedItem �� *SelectedSchoolSubject+� $��2�� �� $��30 ��� $��10 ��� $��30 �.+��� $��Удалить��$	��140��$
��10,0q�$
��Left=�  
ButtonBase#���  ���� *DeleteSchoolSubjectCommand+���� *SelectedSchoolSubject+�� $ ��Редактировать��$	��140��$
��10,0q�$��Right=�#���  ���� *EditSchoolSubjectCommand+���� *SelectedSchoolSubject+�� $��2��$��Удалить все��$��30��$
��10,0q�#���  ���� * DeleteAllSchoolSubjectsCommand+� $��35��$��1��$	��Top=�$��10q� �� $��* ��� $��35 �.+��� 
 � Tag$ 
Поиск��$	��5,0q�  TextBoxBase#���  ���� *
FindText+ �� UpdateSourceTrigger$ PropertyChanged=� � $��1��$��35��$��Right=�$	��Top=����� *LoadAnimation+y���  �� Path$ LoadAnimation� �� 	Converter# [  � $��1��$��Center=�$��Center=�$��10q�$��2�� �� $
��auto � �� $
��auto ��� $
��auto �.+��� $' !Название предмета��$	��350��$��35��$��1��$��10q�#���  ���� $ SchoolSubject.Name�$ PropertyChanged=�.��	 J� InputBindings	 �� 

 �� Key$
 Enter�� �� Command �� *AddSchoolSubjectCommand+ �� CommandParameter �� *SchoolSubject+.��� $��1��$
��10,0q�$��35��.+��� $��Отмена��$	��160��$
��Left=�#���  ���� * CancelEditSchoolSubjectCommand+���� *
EditMode+�� $	��160��$��Right=�#���  ���� *AddSchoolSubjectCommand+���� *SchoolSubject+���� *NameSaveButton+