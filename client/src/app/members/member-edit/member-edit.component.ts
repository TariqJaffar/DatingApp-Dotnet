import { Component, Host, HostListener, OnInit, ViewChild, inject, viewChild } from '@angular/core';
import { Member } from '../../Models/members';
import { AccountService } from '../../_Services/account.service';
import { MembersService } from '../../_Services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PhotoEditorComponent } from "../photo-editor/photo-editor.component";

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule, PhotoEditorComponent],
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'] // Fixed styleUrls
})
export class MemberEditComponent implements OnInit 
{
@ViewChild('editForm') editForm?:NgForm;
  member?: Member;
  @HostListener('window:beforeunload',['$event']) 
  notify($event:any)
  {
    if(this.editForm?.dirty)
    {
      $event.returnValue = true;
    }
  }
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
private toastr=inject(ToastrService)
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    
    // Debugging log to inspect the user object
    console.log('User Object:', user);

    if (!user || !user.userName) {
      console.warn('User or username is undefined');
      return;
    }

    this.memberService.getMember(user.userName).subscribe({
      next: member => {
        this.member = member;
        console.log('Fetched Member:', member); // Log the member data
      },
      error: err => console.error('Error fetching member:', err),
      complete: () => console.log('Member data fetch completed')
    });
  }

updateMember(){
 this.memberService.updatemember(this.editForm?.value)
 .subscribe({
next:_=>{
  this.toastr.success('profile updated successfully');
  this.editForm?.reset(this.member)
}

 })

}

OnMemberChange(event:Member){
  this.member=event;
}
}
