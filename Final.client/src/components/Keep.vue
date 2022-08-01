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
        <img @click="goToProfile" :src="account.picture " class="pfp img-fluid p-0 rounded-pill selectable" alt="">
</template>


<script>
import { AppState } from '../AppState.js'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
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
                await router.push({name: 'Profile', params: {id: props.keep.creator.id}})
            },
            
            async setActive(){
               try{
                keepsService.setActive(props.keep)
               }catch (error){
                logger.error(error)
               }
            },
            account: computed(()=>AppState.account),
            vaults: computed(()=>AppState.vaults),
            keeps: computed(()=>AppState.keeps),
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