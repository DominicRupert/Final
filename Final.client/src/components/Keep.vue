<template>
    <div class="selectable" @click="setActive" data-bs-target="#keep-modal" data-bs-toggle="modal">
<div>
    <p> {{keep.creator.name}}</p>
</div>
    <div class="card bg-dark">
        <h3 class="">{{keep.name}}</h3>


    </div>
    <div>
        <img :src="keep.img" class="img-fluid" alt="">
    </div>
    <div class=" bg-dark">
    </div>
    </div>
        <img @click="goToProfile" :src="keep.creator.picture " class="pfp img-fluid p-0 rounded-pill selectable" alt="">
</template>


<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js'
import { accountService } from '../services/AccountService.js'
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props){
        const router = useRouter()
        return {
            async goToProfile(){
                await router.push({name: 'Profile', params: {id: props.keep.creatorId}})
            },
            
            async setActive(){
               try{
                     await keepsService.setActive(props.keep)
                  
                     
                     
                // keepsService.setActive(props.keep)
                
               }catch (error){
                logger.error(error)
               }
            },
            keeps: computed(()=>AppState.keeps),
            account: computed(()=>AppState.account),
            vaults: computed(()=>AppState.vaults),
        }
    }
}
</script>


<style lang="scss" scoped>
.pfp{
    position: relative;
    top: -100px;
    left: 50px;
    width: 75px;
}


</style>