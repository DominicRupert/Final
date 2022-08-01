<template>
   <Modal id="keep-modal">
   <template #modal-title>{{keep.name}}</template>
    <template #modal-body>
        <div class="row">
            <div class="col-md-6">
                <img :src="keep.img" class="img-fluid py-2" alt="">
                <button @click="addToVault" class="btn btn-dark"> <h3>Add To Vault</h3></button>
            </div>
            <div class="col-md-6">
                <h3>{{keep.name}}</h3>
                <p>{{keep.description}}</p>
                <!-- <p>{{keep.creator.name}}</p> -->
                <!-- <p>{{keep.createdAt}}</p>
                <p>{{keep.updatedAt}}</p> -->
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
        return {
            keep: computed(()=>AppState.keeps),
            vaults: computed(()=>AppState.vaults),
            async addToVault(){
                try{
                    await keepsService.addToVault(this.keep)
                }catch(e){
                    logger.error(e)
                    Pop.toast(e.message)
                }
            } 
        }
    }
}
</script>


<style lang="scss" scoped>

</style>