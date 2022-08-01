<template>
   <Modal id="keep-modal">
   <!-- <template #modal-title>{{keep.name}}</template> -->
    <template #modal-body>
        <div class="container">
            <div class="col-md-6">
                <h3>{{keep.name}}</h3>
                <p>{{keep.description}}</p>
                <!-- <img :src="keep.img" class="img-fluid" alt=""> -->
                <!-- <p @click="goToProfile">{{keep.name}}</p> -->
                <img :src="keep.img" class="img-fluid py-2" alt="">
                <button  class="btn btn-dark"> <h3>Add To Vault</h3></button>
            </div>
            
        </div>
    </template>
   
   </Modal>
</template>


<script>
import { AppState } from '../AppState.js'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'

export default {
 
    setup(){
        const router = useRouter()
        return {
            account: computed(()=>AppState.account),

            keep: computed(()=>AppState.keeps),
            vaults: computed(()=>AppState.vaults),
            user: computed(()=>AppState.user),

            async addToVault(){
                try{
                    await keepsService.addToVault(this.keep)
                }catch(e){
                    logger.error(e)
                    Pop.toast(e.message)
                }
            } ,
            goToProfile(){
                router.push({name: 'Profile', params: {id: props.keep.creatorId}})
            },
        }
    }
}
</script>


<style lang="scss" scoped>

</style>