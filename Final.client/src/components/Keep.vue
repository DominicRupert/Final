<template>
    <div class="border-dark border ">
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
        <h3 class=" p-3">{{keep.description}}</h3>
    </div>
    </div>
</template>


<script>
import { AppState } from '../AppState.js'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(){
        return {
            vaults: computed(()=>AppState.vaults),
            keeps: computed(()=>AppState.keeps),
        }
    }
}
</script>


<style lang="scss" scoped>


</style>