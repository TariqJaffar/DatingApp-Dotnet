import { Component, Inject, OnInit, inject } from '@angular/core';
import { MembersService } from '../../_Services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../Models/members';
import{TabsModule} from 'ngx-bootstrap/tabs';
import { Gallery, GalleryImageComponent, GalleryItemDef, GalleryItemTypes, GalleryModule,GalleryItem, ImageItem } from 'ng-gallery';
@Component({
  selector: 'app-membr-detail',
  standalone: true,
  imports: [TabsModule,GalleryModule,GalleryItemDef],
  templateUrl: './membr-detail.component.html',
  styleUrl: './membr-detail.component.css'
})
export class MembrDetailComponent implements OnInit{
private memberService = inject(MembersService);
private route = inject(ActivatedRoute);
member?:Member;
images:GalleryItem[]=[];

ngOnInit(): void {
  this.loadMember()
 
}

loadMember(){
  const username =this.route.snapshot.paramMap.get('username');
  if(!username) return;
  this.memberService.getMember(username).subscribe({
    next : member=>{
      this.member=member;
      member.photos.map(p=>{
        this.images.push(new ImageItem({src:p.url,thumb:p.url}))
      })

    }
    
  
  
  })

}

}
